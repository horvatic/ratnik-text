using System;

namespace ratnik_text_console
{
    public class ConsoleScreen : IConsoleScreen
    {
        private int xPos;
        private int yPos;
        private int page;
        public ConsoleScreen()
        {
            xPos = 0;
            yPos = 0;
            page = 0;
        }

        public void SetCursorPositionOnNewPage(int newPage)
        {
            xPos = 0;
            yPos = 0;
            page = newPage;
        }

        public void SetCursorPosition(ConsoleKeyInfo c)
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

        public int GetPage()
        {
            return page; 
        }

        private void IncressYPos()
        {
            if (yPos >= System.Console.WindowHeight - 1)
            {
                yPos = 0;
                page++;
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
                if(page > 0)
                {
                    page--;
                }
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
            if (xPos >= System.Console.WindowWidth)
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
