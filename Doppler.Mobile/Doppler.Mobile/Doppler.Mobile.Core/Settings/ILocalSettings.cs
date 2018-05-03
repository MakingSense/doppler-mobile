namespace Doppler.Mobile.Core.Settings
{
    /// <summary>
    /// Handle local settings, which are stored in the device, eg.:
    /// * SharedPreferences in Android
    /// * UserDefaults in iOS
    /// * App Settings in WP
    /// </summary>
    public interface ILocalSettings
    {
        /// <summary> Fast access to data related to the key "IsUserLoggedIn" </summary>
        bool IsUserLoggedIn { get; }

        /// <summary> Fast access to data related to the key "AuthAccessToken" </summary>
        string AuthAccessToken { get; set; }

        /// <summary> Fast access to data related to the key "AccountNameLoggedIn" </summary>
        string AccountNameLoggedIn { get; set; }

        /// <summary> Fast access to data related to the key "Account id logged in" </summary>
        int AccountIdLoggedIn { get; set; }

        /// <summary> Add or update a key with a new value </summary>
        /// <param name="key"> The key is used to refer to an element </param>
        /// <param name="value"> The value is the object which will be stored </param>
        /// <returns> Returns a TRUE value when the add/update has been successful or FALSE if it has not been. </returns>
        bool AddOrUpdateValue<T>(string key, T value);

        /// <summary> Removes all elements from local settings </summary>
        void Clear();

        /// <summary> Returns a value corresponding to the Key </summary>
        /// <param name="key"> The key is used to refer to an element </param>
        /// <param name="defaultValue"> The default value is used in case the key does not have an associated value. </param>
        /// <returns> A value refer by the key or if the key does not have an associated value it will return the default value </returns>
        T GetValueOrDefault<T>(string key, T defaultValue);

        /// <summary> Remove a value corresponding to the Key </summary>
        /// <param name="key"> The key is used to refer to an element </param>
        void Remove(string key);
    }
}
