using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Lexicom.Wpf.AttachedProperties;
public static class DataGrid
{
    /*
     * BindableColumns
     */
    public static readonly DependencyProperty DataGridColumnnsProperty = DependencyProperty.RegisterAttached("DataGridColumns", typeof(ObservableCollection<DataGridColumn>), typeof(DataGrid), new UIPropertyMetadata(null, OnDataGridColumns_DataGrid_PropertyChanged));
    public static ObservableCollection<DataGridColumn> GetDataGridColumns(DependencyObject obj) => (ObservableCollection<DataGridColumn>)obj.GetValue(DataGridColumnnsProperty);
    public static void SetDataGridColumns(DependencyObject obj, ObservableCollection<DataGridColumn> value) => obj.SetValue(DataGridColumnnsProperty, value);
    private static void OnDataGridColumns_DataGrid_PropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        if (source is not System.Windows.Controls.DataGrid dataGrid)
        {
            return;
        }

        dataGrid.Columns.Clear();

        if (e.NewValue is not ObservableCollection<DataGridColumn> columns)
        {
            return;
        }

        foreach (DataGridColumn column in columns)
        {
            dataGrid.Columns.Add(column);
        }

        columns.CollectionChanged += (sender, notifyEventArgs) =>
        {
            if (notifyEventArgs.Action == NotifyCollectionChangedAction.Reset)
            {
                dataGrid.Columns.Clear();

                if (notifyEventArgs.NewItems is not null)
                {
                    foreach (DataGridColumn column in notifyEventArgs.NewItems)
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
            }
            else if (notifyEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                if (notifyEventArgs.NewItems is not null)
                {
                    foreach (DataGridColumn column in notifyEventArgs.NewItems)
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
            }
            else if (notifyEventArgs.Action == NotifyCollectionChangedAction.Move)
            {
                dataGrid.Columns.Move(notifyEventArgs.OldStartingIndex, notifyEventArgs.NewStartingIndex);
            }
            else if (notifyEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                if (notifyEventArgs.OldItems is not null)
                {
                    foreach (DataGridColumn column in notifyEventArgs.OldItems)
                    {
                        dataGrid.Columns.Remove(column);
                    }
                }
            }
            else if (notifyEventArgs.Action == NotifyCollectionChangedAction.Replace)
            {
                if (notifyEventArgs.NewItems is not null)
                {
                    dataGrid.Columns[notifyEventArgs.NewStartingIndex] = notifyEventArgs.NewItems[0] as DataGridColumn;
                }
            }
        };
    }
}
