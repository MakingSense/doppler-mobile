using System;

namespace Doppler.Mobile.Core.Networking
{
    /// <summary> Multi-purpose wrapper for result objects with success and error results </summary>
    /// <typeparam name="TSuccess"> Type returned when operation is successful </typeparam>
    /// <typeparam name="TError"> Type returned when operation failed </typeparam>
    public class Result<TSuccess, TError>
    {
        /// <summary> Initializes a new instance of the <see cref="Result{TSuccess, TError}"/> class for successful operations </summary>
        /// <param name="successValue"> Value returned from a successful operation </param>
        public Result(TSuccess successValue)
        {
            SuccessValue = successValue;
            IsSuccessResult = true;
        }

        /// <summary> Initializes a new instance of the <see cref="Result{TSuccess, TError}"/> class for failed operations </summary>
        /// <param name="errorValue"> Value returned from a failed operation </param>
        public Result(TError errorValue)
        {
            ErrorValue = errorValue;
            IsSuccessResult = false;
        }

        /// <summary> True for successful operations, otherwise false </summary>
        public bool IsSuccessResult { get; }

        /// <summary> Value returned from a successful operation </summary>
        public TSuccess SuccessValue { get; }

        /// <summary> Value returned from a failed operation </summary>
        public TError ErrorValue { get; }
    }
}