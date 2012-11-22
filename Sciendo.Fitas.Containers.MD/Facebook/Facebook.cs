using System;
using Android.App;
using Android.Content;
using Android.Runtime;

namespace Sciendo.Fitas.Container.MD.Facebook
{
    internal class Facebook : IDisposable
    {
        private static IntPtr _class_ref = JNIEnv.FindClass("com/Sciendo/Fitas/FacebookHelper");
        private static IntPtr _methodIdCtor;
        private static IntPtr _methodPostUpdate;
        private static IntPtr _methodSubmitted;
        private static IntPtr _methodOnActivityResult;
        private IntPtr _instance;
        private Activity _activity;
        private bool _disposed = false;

        public Facebook(Activity activity)
        {
            _activity = activity;

            _methodIdCtor = JNIEnv.GetMethodID(_class_ref, "<init>", "()V");
            _methodPostUpdate = JNIEnv.GetMethodID(_class_ref, "PostUpdate", "(Landroid/app/Activity;Ljava/lang/String;)V");
            _methodSubmitted = JNIEnv.GetMethodID(_class_ref, "Submitted", "()Z");
            _methodOnActivityResult = JNIEnv.GetMethodID(_class_ref, "onActivityResult", "(IILandroid/content/Intent;)V");

            IntPtr newObject = JNIEnv.NewObject(_class_ref, _methodIdCtor);
            _instance = JNIEnv.NewGlobalRef(newObject);
        }

        ~Facebook()
        {
            Dispose(false);
        }

        public void PostUpdate(string message)
        {
            IntPtr ptrMessage = JNIEnv.NewString(message);
            JValue[] parms = new JValue[] { new JValue(JNIEnv.ToJniHandle(_activity)), new JValue(ptrMessage) };
            JNIEnv.CallVoidMethod(_instance, _methodPostUpdate, parms);
        }

        public bool Submitted()
        {
            return JNIEnv.CallBooleanMethod(_instance, _methodSubmitted);
        }

        public void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            JValue[] parms = new JValue[] { new JValue(requestCode), new JValue(resultCode), new JValue(JNIEnv.ToJniHandle(data)) };
            JNIEnv.CallVoidMethod(_instance, _methodOnActivityResult, parms);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (_instance != null)
                {
                    JNIEnv.DeleteGlobalRef(_instance);
                    _instance = IntPtr.Zero;
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
