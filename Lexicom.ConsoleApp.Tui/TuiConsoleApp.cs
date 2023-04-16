using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.ConsoleApp.Tui;
public interface ITuiConsoleApp
{
    bool IsClosing { get; }
    void Close();
    /// <exception cref="ArgumentNullException"/>
    Task StartAsync(IServiceProvider provider);
}
public class TuiConsoleApp : ITuiConsoleApp
{
    private readonly IAtlasOperationsProvider _operationsProvider;

    /// <exception cref="ArgumentNullException"/>
    public TuiConsoleApp(IAtlasOperationsProvider operationsProvider)
    {
        ArgumentNullException.ThrowIfNull(operationsProvider);

        _operationsProvider = operationsProvider;
    }

    public bool IsClosing { get; private set; }

    public void Close() => IsClosing = true;

    /// <exception cref="ArgumentNullException"/>
    public async Task StartAsync(IServiceProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        IReadOnlyList<Type> operationTypes = _operationsProvider.OperationTypes;

        var rootPage = new TuiPage();
        for (int index = 1; index <= operationTypes.Count; index++)
        {
            Type operationType = operationTypes[index - 1];

            var definition = new TuiOperationDefinition(operationType);

            rootPage.Add(definition, definition.PageAttribute?.PagePath);
        }

        TuiPage? currentPage = rootPage;
        while (currentPage is not null && !IsClosing)
        {
            Console.Clear();

            string zerothOperation = currentPage.Parent is null ? "quit" : "back";
            Console.WriteLine($"[0]: {zerothOperation}");

            Console.WriteLine();
            Console.WriteLine("Operations:");
            for (int index = 1; index <= currentPage.Definitions.Count; index++)
            {
                TuiOperationDefinition definition = currentPage.Definitions[index - 1];

                Console.WriteLine($"[{index}]: {definition.Title}");
            }

            Console.WriteLine();
            Console.WriteLine("Pages:");
            for (int index = 0; index < currentPage.SubPages.Count; index++)
            {
                TuiPage page = currentPage.SubPages[index];

                Console.WriteLine($"[{currentPage.Definitions.Count + 1 + index}]: {page.Title}");
            }

            bool isValidInput = false;
            while (!isValidInput && !IsClosing)
            {
                Console.WriteLine();
                Console.WriteLine("Enter the [number] of the operation to execute or page to navigate to:");

                string? input = Console.ReadLine();

                if (int.TryParse(input, out int inputIndex))
                {
                    if (inputIndex == 0)
                    {
                        isValidInput = true;
                        currentPage = currentPage?.Parent;
                    }
                    else if (currentPage is not null)
                    {
                        TuiOperationDefinition? definition = currentPage.Definitions.ElementAtOrDefault(inputIndex - 1);

                        if (definition is not null)
                        {
                            isValidInput = true;

                            Console.Clear();
                            Console.WriteLine($"Operation '{definition.Title}' starting");
                            Console.WriteLine();

                            var operation = (ITuiOperation)provider.GetRequiredService(definition.OperationType);

                            string outcomeMessage;
                            try
                            {
                                await operation.ExecuteAsync();

                                outcomeMessage = "finished";
                            }
                            catch (Exception e) when (e is TaskCanceledException or OperationCanceledException)
                            {
                                outcomeMessage = "cancelled";
                            }
                            catch (Exception e)
                            {
                                bool isHandled = await operation.HandleExceptionAsync(e);

                                if (!isHandled)
                                {
                                    Console.WriteLine();
                                    Console.Write(e.Message);
                                    Console.WriteLine();
                                    Console.Write(e.StackTrace);
                                    Console.WriteLine();
                                }

                                outcomeMessage = "failed";
                            }

                            Console.WriteLine();
                            Console.WriteLine($"Operation '{definition.Title}' {outcomeMessage}");
                            Console.WriteLine("Press enter to continue...");

                            if (!IsClosing)
                            {
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            TuiPage? page = currentPage.SubPages.ElementAtOrDefault(inputIndex - currentPage.Definitions.Count - 1);

                            if (page is not null)
                            {
                                isValidInput = true;
                                currentPage = page;
                            }
                        }
                    }
                }

                if (!isValidInput)
                {
                    Console.WriteLine("The value you entered is not valid");
                }
            }
        }
    }
}
