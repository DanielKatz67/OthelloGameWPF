namespace OthelloGameLogic
{
    public class Player
    {
        private readonly eColor r_Color;
        private readonly string r_Name;
        private int m_Score;
        private int m_WinsCount = 0;

        public eColor Color
        {
            get
            {
                return r_Color;
            }
        }
    
        public string Name
        {
            get
            {
                return r_Name;
            }
        }
    
        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public int WinsCount
        {
            get
            {
                return m_WinsCount;
            }
            set
            {
                m_WinsCount = value;
            }
        }

        public Player(string i_Name, int i_Score, eColor i_Color)
        {
            r_Name = i_Name;
            m_Score = i_Score;
            r_Color = i_Color;
        }
    }
}

