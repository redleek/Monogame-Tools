using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame_Tools.Conversation;
using MonoGame_Tools.CharacterLogic;

/*
    The maps and situations are having problems when there is no map, need a default map?
Each situation has hand coded conditions and interactions for each situation.
Situations will cover not only maps, dialog, and win conditions but also dialog between chapters and saving?
 
*/


namespace MonoGame_Tools.Fundamental
{
    public class SituationController
    {
        int CurrentTurn;
        int CurrentSituation;
        public int ActiveController;
        Boolean hasTurnEnded;
        public Boolean AllowCharacterMovement = false;
        MouseState oldMouseState;

        public SituationController()
        {
            CurrentTurn = 0;
            CurrentSituation = 0;
            ActiveController = (int)Constants.CharacterControllerType.Player;
            hasTurnEnded = false;
            oldMouseState = Mouse.GetState();
        }

        //public void loadSituation(int section, MapController mapController, CharacterController characterController, ConversationController conversationController, ContentManager Content, SpriteBatch SP)
        //{
        //    CurrentSituation = section;
        //    conversationController.loadFromLibrary(CurrentSituation, Content, SP);
        //    mapController.loadFromLibrary(CurrentSituation);
        //    characterController.loadCharactersFromLibrary(CurrentSituation, Content);
        //    //conversationController.startConv("test1");
        //    //switch case etc
        //}



        //public void inputLogic(MapController mapController, CharacterController characterController, ConversationController conversationController, ContentManager Content, SpriteBatch SP)
        //{
        //    switch (CurrentSituation) //honestly I need work on this section (it's just a bunch of hazardouse ifs right now)
        //    {
        //        case 0:
        //            /*if (conversationController.Conversations[conversationController.CurrentConversationIndex].InConversation == false && conversationController.Conversations[conversationController.CurrentConversationIndex].ConversationCompleted == false)
        //            {
        //                conversationController.startConv("test1");
        //                AllowCharacterMovement = false;
        //            }
        //            if (conversationController.Conversations[conversationController.CurrentConversationIndex].ConvName == "Victory" && conversationController.Conversations[conversationController.CurrentConversationIndex].ConversationCompleted)
        //            {
        //                CurrentSituation++;
        //                break;
        //            }
        //            if (!conversationController.Conversations[conversationController.CurrentConversationIndex].ConversationCompleted)
        //            {
        //                conversationController.Input();
        //            }
        //            if (conversationController.Conversations[conversationController.CurrentConversationIndex].InConversation == false && mapController.getCurrentMap().IsMapStarted == false)
        //            {
        //                mapController.StartMap("Herbert");
        //                AllowCharacterMovement = true;
        //            }
        //            if (characterController.getCharactersOfController((int)Constants.CharacterController.Enemy).Count == 0) //all enemy characters killed
        //            {
        //                //The match is over?
        //                CurrentTurn = 0;
        //                CurrentTurn += 1;
        //                mapController.EndMap();
        //                conversationController.startConv("Victory");
        //            }
        //            */
        //            //if ()

        //            conversationController.startConvIfNotStarted("test1");
        //            if (conversationController.Conversations[conversationController.CurrentConversationIndex].InConversation == true)
        //            {
        //                AllowCharacterMovement = false;
        //                conversationController.Input();
        //            }
        //            else
        //            {
        //                AllowCharacterMovement = true;
        //                mapController.StartMapIfNotStarted("Herbert");

        //                if (characterController.getCharacterOfController((int)Constants.CharacterControllerType.Enemy).Count == 0)
        //                {
        //                    CurrentTurn = 0;
        //                    mapController.EndMap("Herbert");
        //                    conversationController.startConvIfNotStarted("Victory");
        //                }

        //            }

        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public void Draw(SpriteBatch SP, ConversationController CC, CharacterController ChC, MapController MC, List<Texture2D> LandTiles)
        //{
        //    MC.Draw(SP, LandTiles);
        //    ChC.Draw(SP);
        //    CC.Draw(SP);
        //    //Movement.Draw()
        //}

        public void tryNextTurn(CharacterController CC)
        {
            if (hasTurnEnded)
            {
                nextTurn(CC);
            }
            else
            {
                checkIfTurnCompleted(CC); //at end so precidence goes to the endTurnManually Method, IE if somone wantsto end the turn they use endturn, otherwise it takes 2 checks of the trynextturn to change the turn (assuming the update rate is as it is normal this shouldn't be noticable)
            }
        }

        public void nextTurn(CharacterController CC)
        {
            foreach (Character c in CC.getCharacterOfController(ActiveController))
            {
                c.HasMove = true;
                c.HasAction = true;
            }
            hasTurnEnded = false;
            CurrentTurn++;
            selectNextController();
        }

        public void checkIfTurnCompleted(CharacterController CC) //only a problem if working with threads.
        {
            hasTurnEnded = true;
            foreach (Character c in CC.getCharacterOfController(ActiveController))
            {
                if (c.HasMove)
                {
                    hasTurnEnded = false;
                }
            }
        }

        public void endTurnManually()
        {
            hasTurnEnded = true;
        }


        private void selectNextController()
        {
            if (ActiveController < Enum.GetValues(typeof(Constants.CharacterType)).Length)
            {
                ActiveController++;
            }
            else
            {
                ActiveController = 0;
            }
        }
    }
}