﻿using JewelryStore.Desktop.Models;

namespace JewelryStore.Desktop.ViewModels
{
    public class MetalItemViewModel
    {
        #region Private Fields

        private readonly Metal _metal;

        #endregion

        #region Public Properties

        public byte Id => _metal.Id;

        public string MetalName
        {
            get => _metal.MetalName;
            set => _metal.MetalName = value;
        }

        public int Sample
        {
            get => (int) _metal.Sample;
            set => _metal.Sample = value;
        }

        #endregion

        #region Constructor

        public MetalItemViewModel(Metal metal)
        {
            _metal = metal;
        }

        #endregion
    }
}
