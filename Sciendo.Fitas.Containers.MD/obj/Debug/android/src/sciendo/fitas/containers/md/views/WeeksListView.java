package sciendo.fitas.containers.md.views;


public class WeeksListView
	extends monocross.droid.MXListActivityView_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onListItemClick:(Landroid/widget/ListView;Landroid/view/View;IJ)V:GetOnListItemClick_Landroid_widget_ListView_Landroid_view_View_IJHandler\n" +
			"";
		mono.android.Runtime.register ("Sciendo.Fitas.Containers.MD.Views.WeeksListView, Sciendo.Fitas.Containers.MD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WeeksListView.class, __md_methods);
	}


	public WeeksListView ()
	{
		super ();
		if (getClass () == WeeksListView.class)
			mono.android.TypeManager.Activate ("Sciendo.Fitas.Containers.MD.Views.WeeksListView, Sciendo.Fitas.Containers.MD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onListItemClick (android.widget.ListView p0, android.view.View p1, int p2, long p3)
	{
		n_onListItemClick (p0, p1, p2, p3);
	}

	private native void n_onListItemClick (android.widget.ListView p0, android.view.View p1, int p2, long p3);

	java.util.ArrayList refList;
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
