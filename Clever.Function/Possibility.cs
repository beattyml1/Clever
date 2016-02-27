using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Function
{

    public interface IPossibility<T>
    {
        bool HasValue { get; }
        bool CouldHaveValue { get; }
        T Value { get; }
        IPossibility<T2> GetPossibility<T2>(Func<T, T2> f1);
    }

    public class NullablePossibility<T> : IPossibility<T>
    {
        protected NullablePossibility(T value)
        {
            HasValue = true;
            Value = value;
        }
        protected NullablePossibility()
        {
            HasValue = false;
        }

        public bool HasValue
        {
            get; protected set;
        }

        public T Value
        {
            get; protected set;
        }

        public bool CouldHaveValue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPossibility<T2> GetPossibility<T2>(Func<T, T2> f1)
        {
            return HasValue ? new NullablePossibility<T2>(f1(Value)) : new NullablePossibility<T2>();
        }
    }

    public class NullableStructPossibility<T> : NullablePossibility<T> where T : struct
    {
        private NullableStructPossibility(Nullable<T> nullable)
        {
            HasValue = nullable.HasValue;
            Value = nullable.HasValue ? nullable.Value : default(T);
        }
        public static implicit operator T? (NullableStructPossibility<T> possibility)
        {
            return possibility.HasValue ? new Nullable<T>(possibility.Value) : new Nullable<T>(); 
        }

        public static implicit operator NullableStructPossibility<T>(T? nullable)
        {
            return new NullableStructPossibility<T>(nullable);
        }
    }

    public class TaskPossibility<T> : IPossibility<T>
    {
        private readonly Task<T> task;
        private TaskPossibility(Task<T> task)
        {
            this.task = task;
        }

        public bool CouldHaveValue
        {
            get
            {
                return !(task.IsCanceled || task.IsFaulted);
            }
        }

        public bool HasValue
        {
            get
            {
                return task.IsCompleted;
            }
        }

        public T Value
        {
            get
            {
                return task.Result;
            }
        }

        public IPossibility<T2> GetPossibility<T2>(Func<T, T2> f1)
        {
            return new TaskPossibility<T2>(task.ContinueWith(x => f1(x.Result)));
        }

        public static implicit operator Task<T>(TaskPossibility<T> possibility)
        {
            return possibility.task;
        }

        public static implicit operator TaskPossibility<T>(Task<T> task)
        {
            return  new TaskPossibility<T>(task);
        }
    }
}
