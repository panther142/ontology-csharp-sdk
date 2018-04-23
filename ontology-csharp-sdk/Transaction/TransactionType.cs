using System.Collections.Generic;

namespace TxType
{

    public sealed class TransactionType
    {
        public static readonly TransactionType Bookkeeping = new TransactionType("Bookkeeping", InnerEnum.Bookkeeping, 0x00);
        public static readonly TransactionType Bookkeeper = new TransactionType("Bookkeeper", InnerEnum.Bookkeeper, 0x02);
        public static readonly TransactionType Claim = new TransactionType("Claim", InnerEnum.Claim, 0x03);
        public static readonly TransactionType Enrollment = new TransactionType("Enrollment", InnerEnum.Enrollment, 0x04);
        public static readonly TransactionType Vote = new TransactionType("Vote", InnerEnum.Vote, 0x05);
        public static readonly TransactionType DeployCode = new TransactionType("DeployCode", InnerEnum.DeployCode, 0xd0);
        public static readonly TransactionType InvokeCode = new TransactionType("InvokeCode", InnerEnum.InvokeCode, 0xd1);

        private static readonly IList<TransactionType> valueList = new List<TransactionType>();

        static TransactionType()
        {
            valueList.Add(Bookkeeping);
            valueList.Add(Bookkeeper);
            valueList.Add(Claim);
            valueList.Add(Enrollment);
            valueList.Add(Vote);
            valueList.Add(DeployCode);
            valueList.Add(InvokeCode);
        }

        public enum InnerEnum
        {
            Bookkeeping,
            Bookkeeper,
            Claim,
            Enrollment,
            Vote,
            DeployCode,
            InvokeCode
        }

        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;


        private sbyte _value;
        internal TransactionType(string name, InnerEnum innerEnum, int v)
        {
            _value = (sbyte)v;

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }
        public sbyte value()
        {
            return _value;
        }

        public static TransactionType valueOf(sbyte v)
        {
            foreach (TransactionType e in TransactionType.values())
            {
                if (e._value == v)
                {
                    return e;
                }
            }
            throw new System.ArgumentException();
        }

        public static IList<TransactionType> values()
        {
            return valueList;
        }

        public int ordinal()
        {
            return ordinalValue;
        }

        public override string ToString()
        {
            return nameValue;
        }
    }

}