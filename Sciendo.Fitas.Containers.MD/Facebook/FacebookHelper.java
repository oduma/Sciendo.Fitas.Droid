package com.Sciendo.Fitas; 

import android.util.Log; 
import java.io.IOException; 
import java.net.MalformedURLException; 
import android.util.*; 
import android.app.Activity; 
import android.content.Intent; 
import android.content.SharedPreferences; 
import android.os.Bundle; 
import com.facebook.android.*; 
import com.facebook.android.Facebook.*; 

public class FacebookHelper 
{ 
        private Facebook _facebook = new Facebook("<<< --- YOUR FACEBOOK APP ID --- >>>"); 
        String FILENAME = "AndroidSSO_data"; 
		private SharedPreferences _prefs; 
        private String _message; 
        private boolean _submitted;	


        public void PostUpdate(Activity mainActivity, String message) 
        { 
                Log.i("FITAS","PostUpdate to Facebook :"+message); 

                try 
                { 
                        _submitted=false; 
                        _message=message; 

                        _prefs = mainActivity.getPreferences(android.app.Activity.MODE_PRIVATE); 
                        String access_token = _prefs.getString("access_token", null); 
                        long expires = _prefs.getLong("access_expires", 0); 
                        if(access_token != null) { 
                                _facebook.setAccessToken(access_token); 
                        } 
                        if(expires != 0) { 
                                _facebook.setAccessExpires(expires); 
                        } 

                        /* 
                         * Only call authorize if the access_token has expired. 
                         */ 
                        if(!_facebook.isSessionValid()) { 
                                Log.i("FITAS","Need to Authorize"); 
                                _facebook.authorize(mainActivity, new String[]{"publish_stream"}, Facebook.FORCE_DIALOG_AUTH, new DialogListener() { 
                                                @Override 
                                                public void onComplete(Bundle values) 
                                                {	   
                                                   Log.i("FITAS","Authorize onComplete"); 
                                                   SharedPreferences.Editor editor = _prefs.edit(); 
                                                   editor.putString("access_token", _facebook.getAccessToken()); 
                                                   editor.putLong("access_expires", _facebook.getAccessExpires()); 
                                                   editor.commit();	
                                                   updateStatus(); 
                                                } 
                
                                                @Override 
                                                public void onFacebookError(FacebookError e) { 
                                                        Log.d("FITAS", "FB ERROR. MSG: "+e.getMessage()+", CAUSE: "+e.getCause()); 
                                                } 
                      
                                                @Override 
                                                public void onError(DialogError e) { 
                                                        Log.e("FITAS","AUTH ERROR. MSG: "+e.getMessage()+", CAUSE: "+e.getCause()); 
                                                } 
                      
                                                @Override 
                                                public void onCancel() { 
                                                        Log.d("FITAS", "AUTH CANCELLED"); 
                                                } 
                
                                        }); 
                                }	
                        else 
        updateStatus(); 
                } catch (Exception e){ 
                        Log.e("FITAS","EXCEPTION "+e.getMessage()); 
                } 
        } 

    private void updateStatus(){ 
                Log.i("FITAS","update facebook status"); 
        try { 
            Bundle bundle = new Bundle(); 
            bundle.putString("message", _message); 
            bundle.putString("link", "http://www.sciendo.com/fitas"); 
            bundle.putString("name", "Fitas"); 
            bundle.putString("description", "Get fit with Fitas!"); 
            bundle.putString("picture", "http://www.sciendo.com/fitas/FitasSmall.png");   
            bundle.putString(Facebook.TOKEN, _facebook.getAccessToken()); 
            String response = _facebook.request("me/feed",bundle,"POST"); 
            Log.d("FITAS", "UPDATE RESPONSE -"+response); 
        } catch (Exception e) { 
            Log.e("FITAS","EXCEPTION "+e.getMessage()); 
                }         
                Log.i("FITAS","end update facebook status"); 
                _submitted=true; 
    } 

        public boolean Submitted() 
        { 
                return _submitted; 
        } 

    public void onActivityResult(int requestCode, int resultCode, Intent data) { 
       _facebook.authorizeCallback(requestCode, resultCode, data);             
    } 
        
} 
