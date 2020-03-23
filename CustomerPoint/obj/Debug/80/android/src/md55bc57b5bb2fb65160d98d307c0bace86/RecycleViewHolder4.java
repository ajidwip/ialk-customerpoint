package md55bc57b5bb2fb65160d98d307c0bace86;


public class RecycleViewHolder4
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CustomerPoint.Adapter.RecycleViewHolder4, CustomerPoint", RecycleViewHolder4.class, __md_methods);
	}


	public RecycleViewHolder4 (android.view.View p0)
	{
		super (p0);
		if (getClass () == RecycleViewHolder4.class)
			mono.android.TypeManager.Activate ("CustomerPoint.Adapter.RecycleViewHolder4, CustomerPoint", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
