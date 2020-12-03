namespace Ornette.Application.Converter.Command
{
    public enum Mp3EncodingQuality
    {
        //
        // Summary:
        //     -q 0: Highest quality, slow
        Q0 = 0,
        //
        // Summary:
        //     -q 1
        Q1 = 1,
        //
        // Summary:
        //     -q 2, same as -h
        Q2 = 2,
        //
        // Summary:
        //     -q 3
        Q3 = 3,
        //
        // Summary:
        //     -q 4
        Q4 = 4,
        //
        // Summary:
        //     -q 5, default
        Q5 = 5,
        //
        // Summary:
        //     -q 6
        Q6 = 6,
        //
        // Summary:
        //     -q 7, same as -f
        Q7 = 7,
        //
        // Summary:
        //     -q 8
        Q8 = 8,
        //
        // Summary:
        //     -q 9: poor quality, fast
        Q9 = 9,
        //
        // Summary:
        //     None
        None = 10,
        //
        // Summary:
        //     -f: Fast, equals -q 7
        Speed = 11,
        //
        // Summary:
        //     -h: Default quality, recommended, equals -q 2
        Quality = 12
    }
}