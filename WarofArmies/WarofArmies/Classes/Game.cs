﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarofArmies.Classes
{
    public class Game
    {

        private Army redArmy;        
        private Army blueArmy;
        Label labelwin;
        Weapon sword = DerivedClasses.Sword.giveInstance();
        Armor bronzeArmor = DerivedClasses.BronzeArmor.giveInstance();
        System.Windows.Forms.Form.ControlCollection formControls = (System.Windows.Forms.Form.ControlCollection)Application.OpenForms["Form1"].Controls;

        public Game()
        {
            labelwin = (Label) formControls["label3"];
        }
        public void createArmy()
        {
            redArmy = RedArmy.giveInstance();
            blueArmy = BlueArmy.giveInstance();
            redArmy.createSwordSoldiers(100, sword, bronzeArmor);
            blueArmy.createSwordSoldiers(100, sword, bronzeArmor);
            redArmy.enemy = blueArmy;
            blueArmy.enemy = redArmy;
        }

        public void EnemiesAreFighting()
        {
            if (!isFightOver())
            {
                ProgressBar progresbar1 = (ProgressBar)formControls["progressBar1"];
                ProgressBar progresbar2 = (ProgressBar)formControls["progressBar2"];                
                blueArmy.AttackAll();
                redArmy.AttackAll();
                blueArmy.calculateTotalHealth();
                redArmy.calculateTotalHealth();
                if (blueArmy.soldiers.Count != 0)
                {
                    progresbar1.Value = Convert.ToInt32(blueArmy.totalHealth / blueArmy.soldiers.Count);
                }
                else
                {
                    progresbar1.Value = 0;
                }
                if (redArmy.soldiers.Count != 0)
                {
                    progresbar2.Value = Convert.ToInt32(redArmy.totalHealth / redArmy.soldiers.Count);
                }
                else
                {
                    progresbar2.Value = 0;
                }

            }
            else
            {
                //startWar();
            }
        }

        public bool isFightOver()
        {
            if (redArmy.soldiers.Count == 0 || blueArmy.soldiers.Count == 0)
            {
                winnerArmyShow();
                return true;
            }
            return false;
        }

        

        public string winnerArmyDetection()
        {            
            string winnerArmyName = "";            

            if (blueArmy.soldiers.Count == 0)
            {
                winnerArmyName = "Red Army";
            }
            else if (redArmy.soldiers.Count == 0)
            {
                winnerArmyName = "Blue Army";
            }
            return winnerArmyName;
        }

        public int winnerArmyCount()
        {
            int winnerArmyCount = -1;

            if (blueArmy.soldiers.Count == 0)
            {
                winnerArmyCount = redArmy.soldiers.Count;
            }
            else if (redArmy.soldiers.Count == 0)
            {
                winnerArmyCount = blueArmy.soldiers.Count;
            }
            return winnerArmyCount;
        }

        public void winnerArmyShow()
        {
            labelwin.Text = "";
            labelwin.Text += "Winner is ";
            labelwin.Text += winnerArmyDetection();
            labelwin.Text += " Alive Soldiers Count : ";
            labelwin.Text += winnerArmyCount();
        }


        public void startWar()
        {

            resetGame();
            createArmy();
            enemyDetection();
            
        }

        public void enemyDetection()
        {
            int lowestArmyNumber;
            if (redArmy.soldiers.Count > blueArmy.soldiers.Count)
            {
                lowestArmyNumber = blueArmy.soldiers.Count;
                for (int i = lowestArmyNumber; i < redArmy.soldiers.Count; i++)
                {
                    redArmy.soldiers[i].enemy = blueArmy.soldiers[i % blueArmy.soldiers.Count];
                }
            }
            else
            {
                lowestArmyNumber = redArmy.soldiers.Count;
                for (int i = lowestArmyNumber; i < blueArmy.soldiers.Count; i++)
                {
                    blueArmy.soldiers[i].enemy = redArmy.soldiers[i % redArmy.soldiers.Count];
                }
            }
            for (int i = 0; i < lowestArmyNumber; i++)
            {
                redArmy.soldiers[i].enemy = blueArmy.soldiers[i];
                blueArmy.soldiers[i].enemy = redArmy.soldiers[i];
            }
        }

        public void resetGame()
        {
            if (redArmy !=null && blueArmy!=null)
            {
                redArmy.soldiers.Clear();
                blueArmy.soldiers.Clear();
            }            
        }

        

        

    }
}
