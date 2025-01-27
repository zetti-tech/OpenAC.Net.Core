// ***********************************************************************
// Assembly         : OpenAC.Net.Core
// Author           : RFTD
// Created          : 01-31-2016
//
// Last Modified By : RFTD
// Last Modified On : 08-30-2015
// ***********************************************************************
// <copyright file="EnumExtension.cs" company="OpenAC .Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2014 - 2022 Projeto OpenAC .Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Reflection;

namespace OpenAC.Net.Core.Extensions
{
    /// <summary>
    /// Class EnumExtension.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Retorna a descri��o do enum de acordo com o atributo DescriptionAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            var type = typeof(T);
            Guard.Against<InvalidOperationException>(!type.IsEnum, "O tipo de parametro T precisa ser um enum.");
            Guard.Against<InvalidOperationException>(!Enum.IsDefined(type, value), $"{type} o valor {value} n�o esta definido no enum.");

            var fi = type.GetField(value.ToString(), BindingFlags.Static | BindingFlags.Public);
            if (fi == null) return string.Empty;

            var ret = fi.GetAttribute<DescriptionAttribute>();
            return ret != null ? ret.Description : value.ToString();
        }

        /// <summary>
        /// Retorna a descri��o do enum de acordo com a lista informada.
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="valores"></param>
        /// <param name="retornos"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDescription<T>(this T valor, T[] valores, string[] retornos) where T : struct
        {
            var type = typeof(T);
            Guard.Against<InvalidOperationException>(!type.IsEnum, "O tipo de parametro T precisa ser um enum.");
            Guard.Against<OpenException>(valores.Length != retornos.Length, "O quantidade de valores e retornos s�o diferentes");

            var idx = Array.IndexOf(valores, valor);
            return idx < 0 ? string.Empty : retornos[idx];
        }

        /// <summary>
        /// Retorna o enum de acordo com a lista informada.
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="valores"></param>
        /// <param name="retornos"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToEnum<T>(this string valor, string[] valores, T[] retornos) where T : struct
        {
            Guard.Against<OpenException>(valores.Length != retornos.Length, "O quantidade de valores e retornos s�o diferentes");
            var idx = Array.IndexOf(valores, valor);
            return idx < 0 ? default : retornos[idx];
        }
    }
}