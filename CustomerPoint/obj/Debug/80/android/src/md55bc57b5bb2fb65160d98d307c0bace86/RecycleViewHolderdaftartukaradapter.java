package md55bc57b5bb2fb65160d98d307c0bace86;


public class RecycleViewHolderdaftartukaradapter
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CustomerPoint.Adapter.RecycleViewHolderdaftartukaradapter, CustomerPoint", RecycleViewHolderdaftartukaradapter.class, __md_methods);
	}


	public RecycleViewHolderdaftartukaradapter (android.view.View p0)
	{
		super (p0);
		if (getClass () == RecycleViewHolderdaftartukaradapter.class)
			mono.android.TypeManager.Activate ("CustomerPoint.Adapter.RecycleViewHolderdaftartukaradapter, CustomerPoint", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
