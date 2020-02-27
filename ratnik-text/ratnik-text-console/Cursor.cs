using System;

namespace ratnik_text_console
{
    public class Cursor : ICursor
    {
        private int xPos;
        private int yPos;

        public Cursor()
        {
            xPos = 0;
            yPos = 0;
        }

        public void SetPos(ConsoleKeyInfo c)
        {
            if(c.Key == System.ConsoleKey.Enter)
            {
                IncressYPos();
            }
            else if(c.Key == System.ConsoleKey.Backspace)
            {
                if(xPos == 0 && yPos > 0)
                {
                    DecressYPos();
                    xPos = System.Console.WindowWidth - 1;
                }
                else if(xPos != 0 || yPos != 0)
                {
                    DecressXPos();
                }
            }
            else if (c.Key == System.ConsoleKey.LeftArrow)
            {
                DecressXPos();
            }
            else if (c.Key == System.ConsoleKey.RightArrow)
            {
                IncressXPos();
            }
            else if (c.Key == System.ConsoleKey.UpArrow)
            {
                DecressYPos();
            }
            else if (c.Key == System.ConsoleKey.DownArrow)
            {
                IncressYPos();
            }
            else
            {
                IncressXPos();
            }
            Console.SetCursorPosition(xPos, yPos);
        }

        public (int x, int y) GetCursorPosition()
        {
            return (xPos, yPos);
        }

        private void IncressYPos()
        {
            if (yPos + 1 > System.Console.WindowHeight)
            {
                yPos = System.Console.WindowHeight;
            } 
            else
            {
                yPos++;
            }
            xPos = 0;
        }

        private void DecressYPos()
        {
            if (yPos - 1 < 0)
            {
                yPos = 0;
            }
            else
            {
                yPos--;
            }
            xPos = 0;
        }

        private void IncressXPos()
        {
            if (xPos + 1 >= System.Console.WindowWidth)
            {
                IncressYPos();
            }
            else
            {
                xPos++;
            }
        }

        private void DecressXPos()
        {
            if (xPos - 1 < 0)
            {
                xPos = 0;
            }
            else
            {
                xPos--;
            }
        }
    }
}
