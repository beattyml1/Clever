using Clever.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Service
{
    public class ServiceOption<T>: IPossibility<T>
    {
        private ServiceOptionState<T> state = new ServiceOptionState<T>();

        // Constructors
        private ServiceOption()
        {
            state.Errors = new ServiceError[0];
        }

        public ServiceOption(T value) : this()
        {
            state.Value = value;
        }

        public ServiceOption(ServiceError error) : this()
        {
            state.Errors =  new [] { error }; 
        }

        public ServiceOption(IEnumerable<ServiceError> errors) : this()
        {
            state.Errors = errors.ToArray();
        }

        public ServiceOption(Task<T> promise) : this()
        {
            state.promise = promise;
        }

        // Propties
        public T Value
        {
            get { return HasError ? default(T) : IsDelayed ? Promise.Result : state.Value; }
            private set
            {
                state.Value = value;
                state.HasValue = true;
            }
        }

        public bool HasValue { get { return state.HasValue || (IsDelayed && Promise.IsCompleted); } }

        public bool IsDelayed { get { return state.promise != null; } }

        public Task<T> Promise { get { return state.promise; } }

        public string ErrorMessage { get { return Error?.Message; } }

        public ServiceError Error { get { return ErrorList.FirstOrDefault(); } }

        public string[] ErrorMessageList { get { return ErrorList.Select(x => x.Message).ToArray(); } }

        public ServiceError[] ErrorList { get { return IsDelayed ? MakeErrorsFor(Promise.Exception) : state.Errors; } }

        public bool HasError { get { return state.Errors.Any() || (IsDelayed && (Promise.IsCanceled || Promise.IsCompleted)); } }

        public bool HasMultipleErrors { get { return state.Errors.Count() > 1; } }

        public bool CouldHaveValue
        {
            get
            {
                return !HasError && !Promise.IsFaulted && !Promise.IsCanceled;
            }
        }


        // Operators
        public static implicit operator ServiceOption<T>(T value)
        {
            return new ServiceOption<T>(value);
        }

        public static implicit operator ServiceOption<T>(ServiceError error)
        {
            return new ServiceOption<T>(error);
        }

        public static implicit operator ServiceOption<T>(ServiceError[] errors)
        {
            return new ServiceOption<T>(errors);
        }

        public static implicit operator ServiceOption<T>(Task<T> promise)
        {
            return new ServiceOption<T>(promise);
        }


        // Functions 
        private ServiceError[] MakeErrorsFor(AggregateException exception)
        {
            return new ServiceError[] { new ServiceError { Message = Promise.Exception.Data.ToString() } }; //TODO: This is definitely not right
        }

        public ServiceOption<T2> PossiblyTransform<T2>(Func<T, T2> f1)
        {
            return HasValue ? new ServiceOption<T2>(f1(Value)) :
                   HasError ? HasMultipleErrors ? new ServiceOption<T2>(ErrorList) : new ServiceOption<T2>(Error) :
                   IsDelayed ? new ServiceOption<T2>(Promise.ContinueWith(x => f1(x.Result))) : new ServiceOption<T2>(Error);
        }

        public IPossibility<T2> GetPossibility<T2>(Func<T, T2> f1)
        {
            return PossiblyTransform(f1);
        }
    }

    public struct ServiceOptionState<T>
    {
        public T Value;
        public bool HasValue;
        public Task<T> promise;
        public ServiceError[] Errors;
    }

    public class ServiceError
    {
        public static ServiceError New(string message)
        {
            return new ServiceError { Message = message };
        }

        public string Message { get; set; }
    }
}
