using System;
using UnityEngine;
using Zenject;

namespace States
{
    public class PlayingState : IGameState
    {
        private readonly MovePlayerSignal _movePlayerSignal;
        private IGameState _gameStateImplementation;

        public PlayingState(MovePlayerSignal movePlayerSignal)
        {
            _movePlayerSignal = movePlayerSignal;
        }

        public void Tick(IGameContext gameContext)
        {
            // do nothing
        }

        public void FixedTick(IGameContext gameContext)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            _movePlayerSignal.Fire(moveHorizontal);
        }

        public class Factory : Factory<PlayingState>
        {
        }
    }
}
