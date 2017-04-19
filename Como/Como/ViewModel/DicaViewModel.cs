﻿using Como.Data;
using Como.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Como.ViewModel
{
    public class DicaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Dica> Dicas { get { return _dicas; } set { _dicas = value; OnPropertyChanged("Dicas"); } }
        private ObservableCollection<Dica> _dicas = new ObservableCollection<Dica>();

        public RepositoryIterator RepositoryIterator;

        public DicaViewModel(RepositoryIterator repositoryIterator)
        {
            RepositoryIterator = repositoryIterator;
        }

        public void ObterDicas()
        {
            var lista = new List<Dica>();
            bool encontrou = false;

            while (!encontrou && RepositoryIterator.HasNext())
            {
                IRepository iRepository = (IRepository)RepositoryIterator.Next();
                lista = iRepository.ObterDicas();
                if (lista != null && lista.Count > 0)
                {
                    encontrou = true;
                }
            }
            RepositoryIterator.resetPosition();

            for (int index = 0; index < lista.Count; index++)
            {
                var dica = lista[index];

                if (index + 1 > Dicas.Count || Dicas[index].Equals(dica))
                {
                    Dicas.Insert(index, dica);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }
}