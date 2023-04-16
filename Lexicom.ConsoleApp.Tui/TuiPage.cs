namespace Lexicom.ConsoleApp.Tui;
internal class TuiPage
{
    private readonly List<TuiOperationDefinition> _operationDefinitions;
    private readonly List<TuiPage> _subPages;

    public TuiPage() : this(Guid.NewGuid().ToString(), null)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public TuiPage(
        string title, 
        TuiPage? parent)
    {
        ArgumentNullException.ThrowIfNull(title);

        Title = title;
        Parent = parent;

        _operationDefinitions = new List<TuiOperationDefinition>();
        _subPages = new List<TuiPage>();
    }

    public string Title { get; }
    public TuiPage? Parent { get; }
    public IReadOnlyList<TuiOperationDefinition> Definitions => _operationDefinitions;
    public IReadOnlyList<TuiPage> SubPages => _subPages;

    /// <exception cref="ArgumentNullException"/>
    public void Add(TuiOperationDefinition definition, string? pagePath = null)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (string.IsNullOrWhiteSpace(pagePath))
        {
            _operationDefinitions.Add(definition);

            return;
        }

        string[] pagePathParts = pagePath.Split('/', 2);

        if (pagePathParts.Length <= 0)
        {
            _operationDefinitions.Add(definition);

            return;
        }

        string pageName = pagePathParts[0];
        string? remainingPath = null;

        if (pagePathParts.Length > 1)
        {
            remainingPath = pagePathParts[1];
        }

        TuiPage? subPage = SubPages.FirstOrDefault(p => p.Title == pageName);

        if (subPage is null)
        {
            subPage = new TuiPage(pageName, this);

            _subPages.Add(subPage);
        }

        subPage.Add(definition, remainingPath);
    }
}