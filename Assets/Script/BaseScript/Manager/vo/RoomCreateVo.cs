using System;

namespace AssemblyCSharp
{
    [Serializable]
    public class RoomCreateVo
    {
        public bool hong;
        public int ma;
        public int roomId;

        public int roomType; //1转转；2、划水；3、长沙

        /*局数*/
        public int roundNumber;
        public bool sevenDouble;
        public int ziMo; //1：自摸胡；2、抢杠胡
        public int xiaYu;
        public string name;
        public bool addWordCard;
        public int magnification;
        public byte bPlayCoutIdex; //玩家局数0 1，  8 或者16局
        public uint dwPlayCout; //游戏局数
        public uint dwPlayTotal; //总局数

        public RoomCreateVo()
        {
        }
    }
}