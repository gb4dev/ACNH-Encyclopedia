package crc64051efa6251863288;


public class NavigationPageCustomRenderer
	extends crc64720bb2db43a66fe9.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewAdded:(Landroid/view/View;)V:GetOnViewAdded_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("ACNHEncyclopedia.Droid.ServicesAndroid.NavigationPageCustomRenderer, ACNHEncyclopedia.Android", NavigationPageCustomRenderer.class, __md_methods);
	}


	public NavigationPageCustomRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == NavigationPageCustomRenderer.class)
			mono.android.TypeManager.Activate ("ACNHEncyclopedia.Droid.ServicesAndroid.NavigationPageCustomRenderer, ACNHEncyclopedia.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public NavigationPageCustomRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == NavigationPageCustomRenderer.class)
			mono.android.TypeManager.Activate ("ACNHEncyclopedia.Droid.ServicesAndroid.NavigationPageCustomRenderer, ACNHEncyclopedia.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public NavigationPageCustomRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == NavigationPageCustomRenderer.class)
			mono.android.TypeManager.Activate ("ACNHEncyclopedia.Droid.ServicesAndroid.NavigationPageCustomRenderer, ACNHEncyclopedia.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onViewAdded (android.view.View p0)
	{
		n_onViewAdded (p0);
	}

	private native void n_onViewAdded (android.view.View p0);

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
