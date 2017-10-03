using System.Collections.Generic;

namespace NetMock.Utils
{
    public static class Extensions
    {
	    public static TItem AddAndReturn<TList, TItem>(this List<TList> list, TItem item)
			where TItem : TList
		{
		    list.Add(item);
		    return item;
	    }
    }
}
