package sciendo.fitas.containers.md.views;


public class WeekView
	extends monocross.droid.MXDialogActivityView_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Sciendo.Fitas.Containers.MD.Views.WeekView, Sciendo.Fitas.Containers.MD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WeekView.class, __md_methods);
	}


	public WeekView ()
	{
		super ();
		if (getClass () == WeekView.class)
			mono.android.TypeManager.Activate ("Sciendo.Fitas.Containers.MD.Views.WeekView, Sciendo.Fitas.Containers.MD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
