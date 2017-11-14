using System;
using UnityEngine;

namespace GameStates
{
    public class EmptyState : GameState<GameMng>
    {
        public override void Enter(GameMng entity)
        {
            STATE_NAME = "EMPTY";
        }

        public override void Exit(GameMng entity)
        {
        }

        public override void Touch(GameMng entity, float x, float y)
        {
        }

        public override void Update(GameMng entity)
        {
        }
    }

    public class LobbyState : GameState<GameMng>
    {

        public override void Enter(GameMng entity)
        {
            STATE_NAME = "LOBBY";
        }
        public override void Update(GameMng entity)
        {
        }
        public override void Exit(GameMng entity)
        {
        }
        public override void Touch(GameMng entity, float x, float y)
        {
        }
    }
    public class PlayState : GameState<GameMng>
    {
        public override void Enter(GameMng entity)
        {
            STATE_NAME = "PLAY";
        }

        public override void Exit(GameMng entity)
        {
        }

        public override void Touch(GameMng entity, float x, float y)
        {
        }

        public override void Update(GameMng entity)
        {
        }
    }
    public class LevelClearState : GameState<GameMng>
    {
        public override void Enter(GameMng entity)
        {
            STATE_NAME = "LEVEL_CLEAR";
        }

        public override void Exit(GameMng entity)
        {
        }

        public override void Touch(GameMng entity, float x, float y)
        {
        }

        public override void Update(GameMng entity)
        {
        }
    }
    public class ScoreCalState : GameState<GameMng>
    {
        public override void Enter(GameMng entity)
        {
            STATE_NAME = "SCORE_CAL";
        }

        public override void Exit(GameMng entity)
        {
        }

        public override void Touch(GameMng entity, float x, float y)
        {
        }

        public override void Update(GameMng entity)
        {
        }
    }
}
