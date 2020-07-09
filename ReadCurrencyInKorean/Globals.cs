using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCurrencyInKorean
{
    class Globals
    {
        public static string CurrencyString = "0";

        /// <summary>
        /// 숫자의 우리말 문자열 반환
        /// </summary>
        /// <returns></returns>
        public static string ReadNumber()
        {
            string resultStr = string.Empty;

            // 4자리 단위로 숫자를 끊어 배열에 저장
            string newCurrecyStr = string.Empty;
            int separate = 1;
            for (int digit = 0; digit < CurrencyString.Length; digit++)
            {
                newCurrecyStr =
                    CurrencyString.Substring(CurrencyString.Length - digit - 1, 1) + newCurrecyStr;
                if (separate == 4 && digit != CurrencyString.Length - 1)
                {
                    newCurrecyStr = "," + newCurrecyStr;
                    separate = 1;
                }
                else { separate++; }
            }
            int[] currencyArray = (from numStr in newCurrecyStr.Split(new char[] { ',' })
                                   select int.Parse(numStr)).Reverse().ToArray();

            // 만 단위로 읽은 한글 문자열 생성
            string[] digitDescription = { "", "만", "억", "조", "경" };

            for (int idx = 0; idx < currencyArray.Length; idx++)
            {
                string readIt = ReadBaseNumber(currencyArray[idx]);
                if (readIt != "")
                {
                    resultStr = $"{readIt}{digitDescription[idx]} " + resultStr;
                }
            }

            // 읽은 한글 문자열이 빈문자열인 경우(==0) "영" 반환
            if (resultStr == "") { resultStr = "영"; }

            return resultStr.Trim();
        }


        /// <summary>
        /// 천단위 숫자까지 한글로 읽은 문자열을 반환(0인 경우 빈 문자열 반환)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string ReadBaseNumber(int number)
        {
            string resultStr = string.Empty;

            string[] num2Str =
            { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };

            string[] digitDescription = { "", "십", "백", "천" };

            string numStr = number.ToString();
            for (int digit = 0; digit < numStr.Length; digit++)
            {
                string aNumStr = numStr.Substring(numStr.Length - digit - 1, 1);
                int aNumStrIndex = int.Parse(aNumStr);
                // "일"천, "일"백, "일"십 자리일 경우 "일" => ""
                string aNumRead =
                    (digit > 0 && aNumStrIndex == 1) ? "" : num2Str[aNumStrIndex];
                // "영"인 경우 자리 수 표시 안함 (영백, 영십, 영 => X)
                if (aNumStrIndex != 0)
                {
                    aNumRead += digitDescription[digit];
                }
                resultStr = aNumRead + resultStr;
            }
            return resultStr;
        }
    }
}
