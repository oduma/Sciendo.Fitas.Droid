package android.dialog;


public class DialogInstanceData
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Android.Dialog.DialogInstanceData, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DialogInstanceData.class, __md_methods);
	}


	public DialogInstanceData ()
	{
		super ();
		if (getClass () == DialogInstanceData.class)
			mono.android.TypeManager.Activate ("Android.Dialog.DialogInstanceData, Android.Dialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
