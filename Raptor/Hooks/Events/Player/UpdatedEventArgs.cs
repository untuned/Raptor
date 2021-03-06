﻿using System;
using System.ComponentModel;
using JetBrains.Annotations;
using Terraria;

namespace Raptor.Hooks.Events.Player
{
    /// <summary>
    ///     Provides data for the <see cref="PlayerHooks.Update" /> and <see cref="PlayerHooks.Update2" /> events.
    /// </summary>
    [PublicAPI]
    public sealed class UpdatedEventArgs : HandledEventArgs
    {
        internal UpdatedEventArgs(Terraria.Player player)
        {
            Player = player;
        }

        /// <summary>
        ///     Gets a value indicating whether the player is the local player.
        /// </summary>
        public bool IsLocal => Player.whoAmI == Main.myPlayer;

        /// <summary>
        ///     Gets the player.
        /// </summary>
        [CLSCompliant(false)]
        [NotNull]
        public Terraria.Player Player { get; }
    }
}
