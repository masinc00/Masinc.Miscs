using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Masinc.Miscs
{
    //https://qiita.com/soi/items/d0c83a0cc3a4b23237ef
    public static partial class MiscExtensions
    {
        /// <summary>
        /// イベントを発行する
        /// </summary>        
        /// <param name="@this">送信元インスタンス</param>
        /// <param name="propertyName">プロパティ名を表すExpression。() => Nameのように指定する。</param>
        public static void Raise(this PropertyChangedEventHandler h, object @this = null, [CallerMemberName]string propertyName = null)
        {
            if (h == null) return;
            h(@this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
