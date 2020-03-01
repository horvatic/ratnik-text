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

        public void SetCursorPositionOnNewPage()
        {
            xPos = 0;
            yPos = 0;
        }

        public void SetEnter()
        {
            IncressYPos();
        }

        public void SetChar()
        {
            IncressXPos();
        }

        public void SetBackspace(int x)
        {
            if (xPos == 0 && yPos > 0)
            {
                DecressYPos();
                xPos = x;
            }
            else if (xPos != 0 || yPos != 0)
            {
                DecressXPos();
            }
        }

        public void SetLeftArrow()
        {
            SetBackspace(1);
        }

        public void SetRightArrow()
        {
            IncressXPos();
        }

        public void UnsetLeftArrow()
        {
            SetRightArrow();
        }

        public void UnsetRightArrow()
        {
            SetLeftArrow();
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
            if (xPos >= System.Console.WindowWidth - 1)
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
                DecressYPos();
            }
            else
            {
                xPos--;
            }
        }
    }
}
