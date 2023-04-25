namespace Lexicom.ConsoleApp.Tui;
internal class TuiPage
{
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
    private List<TuiOperationDefinition> _operationDefinitions;
    public IReadOnlyList<TuiOperationDefinition> Definitions => _operationDefinitions;

    private List<TuiPage> _subPages;
    public IReadOnlyList<TuiPage> SubPages => _subPages;

    /// <exception cref="ArgumentNullException"/>
    public void Add(TuiOperationDefinition definition, string? pagePath = null)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (string.IsNullOrWhiteSpace(pagePath))
        {
            AddAndOrder(definition);

            return;
        }

        string[] pagePathParts = pagePath.Split('/', 2);

        if (pagePathParts.Length <= 0)
        {
            AddAndOrder(definition);

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

            AddAndOrder(subPage);
        }

        subPage.Add(definition, remainingPath);

        
    }

    private void AddAndOrder(TuiOperationDefinition definition)
    {
        _operationDefinitions.Add(definition);

        _operationDefinitions = _operationDefinitions
            .OrderBy(d => d.PriorityAttribute is not null ? d.PriorityAttribute.Priority : TuiPriorityAttribute.DEFAULT_PRIORITY)
            .ThenBy(d => d.Title)
            .ToList();
    }

    private void AddAndOrder(TuiPage subPage)
    {
        _subPages.Add(subPage);

        _subPages = _subPages
            .OrderBy(p => p.Title)
            .ToList();
    }
}