//  Паралельне програмування
//  Лабораторна робота ЛР03. ПОТОКИ В МОВІ С#
//
//  F1: 1.19 [d = MAX(B + C) + MIN(A + B*(MA*ME))]
//  F2: 2.16 [ML = SORT(TRANS(MF)*MK)]
//  F3: 3.21 [S = SORT(O*MO)*(MS *MT)]
//
// Даценко Максим, ІО-04


using Data;

namespace PP;

internal class Program
{
    static void Main(string[] args)
    {
        ThreadTools.StartT1();
        ThreadTools.StartT2();
        ThreadTools.StartT3();
    }
}