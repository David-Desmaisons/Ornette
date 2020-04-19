﻿using Ornette.Application.Model;
using Ornette.Application.ViewModel;

namespace Ornette.ViewModel
{
    /// <summary>
    /// ViewModel for the "main" route
    /// </summary>
    public class MainViewModel : Neutronium.BuildingBlocks.ViewModel
    {
        public PlayerViewModel Player { get; }

        public MainViewModel(PlayerViewModel player)
        {
            Player = player;
        }
    }
}
