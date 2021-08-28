﻿using System.Diagnostics;

// pseudo discriminated unions
namespace InterReact
{
    public abstract class Union
    {
        public object Source { get; init; }
        public Union(object source) => Source = source;
    }

    public sealed class Union<T1, T2> : Union
        where T1 : notnull
        where T2 : notnull
    {
        public Union(object source) : base(source)
        {
            Debug.Assert(source is T1 or T2);
        }
    }

    public sealed class Union<T1, T2, T3> : Union
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        public Union(object source) : base(source)
        {
            Debug.Assert(source is T1 or T2 or T3);
        }
    }

    public sealed class Union<T1, T2, T3, T4> : Union
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        public Union(object source) : base(source)
        {
            Debug.Assert(source is T1 or T2 or T3 or T4);
        }
    }

}
