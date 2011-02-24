// The MIT License
// 
// Copyright (c) 2010-2011 Tim Gerundt <tim@gerundt.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections;
using System.Windows.Forms;

public class ListViewItemComparer : IComparer
{
    private bool m_UseTagObject;
    private int m_SortColumn;
    private SortOrder m_SortOrder;
    private CaseInsensitiveComparer m_Comparer;

    /// <summary>
    /// ...
    /// </summary>
    public ListViewItemComparer()
    {
        m_UseTagObject = false;
        m_SortColumn = 0;
        m_SortOrder = SortOrder.None;
        m_Comparer = new CaseInsensitiveComparer();
    }

    /// <summary>
    /// ...
    /// </summary>
    /// <value>Use tag object?</value>
    /// <returns>Use tag object?</returns>
    public bool UseTagObject
    {
        get { return m_UseTagObject; }
        set { m_UseTagObject = value; }
    }

    /// <summary>
    /// ...
    /// </summary>
    /// <value>Sort column</value>
    /// <returns>Sort column</returns>
    public int SortColumn
    {
        get { return m_SortColumn; }
        set { m_SortColumn = value; }
    }

    /// <summary>
    /// ...
    /// </summary>
    /// <value>Sort order</value>
    /// <returns>Sort order</returns>
    public SortOrder SortOrder
    {
        get { return m_SortOrder; }
        set { m_SortOrder = value; }
    }

    /// <summary>
    /// Guess sort order for column.
    /// </summary>
    /// <param name="sortColumn">Sort column</param>
    public void GuessSortOrder(int sortColumn)
    {
        if (sortColumn == m_SortColumn) //If column already sorted...
        {
            m_SortOrder = ReverseSortOrder(m_SortOrder);
        }
        else //If column NOT already sorted...
        {
            m_SortColumn = sortColumn;
            m_SortOrder = SortOrder.Ascending;
        }
    }

    /// <summary>
    /// Reverse sort order.
    /// </summary>
    /// <param name="sortOrder">Sort order</param>
    /// <returns>Reversed sort order</returns>
    public static SortOrder ReverseSortOrder(SortOrder sortOrder)
    {
        if (sortOrder == SortOrder.Ascending) //If ascending...
        {
            return SortOrder.Descending;
        }
        else //If NOT ascending...
        {
            return SortOrder.Ascending;
        }
    }

    #region IComparer
    /// <summary>
    /// ...
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>Compare result</returns>
    public int Compare(object x, object y)
    {
        try
        {
            ListViewItem.ListViewSubItem oListSubItemX = GetListViewSubItem(x, m_SortColumn);
            ListViewItem.ListViewSubItem oListSubItemY = GetListViewSubItem(y, m_SortColumn);
            int oCompareResult = 0;

            if (m_UseTagObject && oListSubItemX.Tag != null && oListSubItemY.Tag != null) //If use tag object...
            {
                oCompareResult = m_Comparer.Compare(oListSubItemX.Tag, oListSubItemY.Tag);
            }
            else //If NOT use tag object...
            {
                oCompareResult = m_Comparer.Compare(oListSubItemX.Text, oListSubItemY.Text);
            }

            if (m_SortOrder == SortOrder.Ascending) //If ascending...
            {
                return oCompareResult;
            }
            else if (m_SortOrder == SortOrder.Descending) //If descending...
            {
                return -oCompareResult;    
            }
            else //If NOT ascending/descending...
            {
                return 0;
            }
        }
        catch
        {
            return 0;
        }
    }
    #endregion

    /// <summary>
    /// ...
    /// </summary>
    /// <param name="listItem">List view item</param>
    /// <param name="column">Column</param>
    /// <returns>List view sub item</returns>
    /// <exception cref="System.ArgumentException">Thrown when <paramref name="listItem"/> is no list view item.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <paramref name="column"/> is out of range.</exception>
    private ListViewItem.ListViewSubItem GetListViewSubItem(object listItem, int column)
    {
        ListViewItem oListItem = listItem as ListViewItem;

        if (oListItem == null) //If NO list view item...
        {
            throw new ArgumentException("Object is no list view item.", "listItem");
        }

        if (column < 0 & column > oListItem.SubItems.Count) //If column out of range...
        {
            throw new ArgumentOutOfRangeException("column");
        }

        return oListItem.SubItems[column];
    }
}