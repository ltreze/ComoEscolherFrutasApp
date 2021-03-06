﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace Como.Model
{
    [DataContract]
    public class Dica : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "idDica")]
        public int ID { get; set; }

        [DataMember(Name = "nomeFruta")]
        public string NomeFruta { get; set; }

        [DataMember(Name = "descricao")]
        public string Descricao { get; set; }

        [DataMember(Name = "nomeArquivo")]
        public string NomeArquivo { get { return nomeArquivo; } set { nomeArquivo = value; OnPropertyChanged("NomeArquivo"); OnPropertyChanged("UrlImagem"); } }
        private string nomeArquivo;

        [Ignore]
        [DataMember(Name = "imagem")]
        public Imagem Imagem { get; set; }

        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        [Ignore]
        public string CaminhoArquivo { get { return App.Helper.ObterCaminhoPadraoDevice(nomeArquivo); } }

        [Ignore]
        public Image ImagemBinding { get; set; }

        [Ignore]
        public ImageSource ImagemBindingSource { get { return this.ImagemBinding.Source; } }


        public Dica()
        {
            this.ImagemBinding = new Image();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }

    [DataContract]
    public class Imagem
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "data")]
        public byte[] Data { get; set; }

    }

    public class DebugHelper
    {
        public void Print<T>(IEnumerable<T> lista)
        {
            Debug.WriteLine(" ****** * * * * *  DEBUG PRINT * * * ******** ");

            Type typeParameterType = typeof(T);

            foreach (var obj in lista)
            {
                PrintProperties(obj, 4);
                Debug.WriteLine("- ");
            }
        }

        public void PrintProperties(object obj, int indent)
        {
            if (obj == null) return;
            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            var properties = objType.GetRuntimeProperties();
            Debug.WriteLine("{0}*{1}", indentString, objType.Name);
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);

                if (propValue.GetType() == typeof(string) || propValue.GetType() == typeof(int)
                    || propValue.GetType() == typeof(decimal) || propValue.GetType() == typeof(float)
                    || propValue.GetType() == typeof(byte) || propValue.GetType() == typeof(long)
                    || propValue.GetType() == typeof(char) || propValue.GetType() == typeof(bool)
                    || propValue.GetType() == typeof(short) || propValue.GetType() == typeof(sbyte)
                    || propValue.GetType() == typeof(uint) || propValue.GetType() == typeof(Guid))
                {
                    Debug.WriteLine("{0}*{1}: {2}", indentString, property.Name, propValue);
                }
                else
                {
                    PrintProperties(propValue, indent * 2);
                }
            }
        }
    }
}