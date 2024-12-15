namespace AdventOfCode.Solutions.Year2024.Day09;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/9">
/// Day 09: Disk Fragmenter.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    public Solution() : base(2024, 09) { }

    public override object SolvePartOne()
    {
        List<int> diskmap = CreateDiskmap(Input);

        CompactDisk(diskmap);

        return GetChecksum(diskmap);
    }

    public override object SolvePartTwo()
    {
        List<int> diskmap = CreateDiskmap(Input);

        CompressDiskByWholeFiles(diskmap);

        return GetChecksum(diskmap);
    }

    private static List<int> CreateDiskmap(string input)
    {
        List<int> blocks = new();

        for (int idx = 0, fileId = 0; idx < input.Length; idx++)
        {
            int blockSize = int.Parse(input[idx].ToString());
            int blockValue = idx % 2 == 0 ? fileId++ : -1;

            blocks.AddRange(Enumerable.Repeat(blockValue, blockSize));
        }

        return blocks;
    }

    private static void CompactDisk(List<int> blocks)
    {
        int leftIdx = 0, rightIdx = blocks.Count - 1;

        while (leftIdx < rightIdx)
        {
            while (blocks[leftIdx] != -1) leftIdx++;
            while (blocks[rightIdx] == -1) rightIdx--;

            if (leftIdx < rightIdx)
            {
                blocks[leftIdx] = blocks[rightIdx];
                blocks[rightIdx] = -1;
                leftIdx++;
                rightIdx--;
            }
        }
    }

    private static void CompressDiskByWholeFiles(List<int> blocks)
    {
        List<List<int>> groupedBlocks = GroupByContiguous(blocks);
        int leftIdxWithFreeSpace = 0, rightIdx = groupedBlocks.Count - 1;

        while (leftIdxWithFreeSpace < rightIdx)
        {
            if (!groupedBlocks[leftIdxWithFreeSpace].Any(n => n == -1) || groupedBlocks[rightIdx][0] == -1)
            {
                while (!groupedBlocks[leftIdxWithFreeSpace].Any(n => n == -1)) leftIdxWithFreeSpace++;
                while (groupedBlocks[rightIdx][0] == -1) rightIdx--;
                continue;
            }

            for (int leftIdx = leftIdxWithFreeSpace; leftIdx < rightIdx; leftIdx++)
            {
                if (groupedBlocks[rightIdx].Count > groupedBlocks[leftIdx].Count)
                {
                    continue;
                }

                int freeSpaceSize = groupedBlocks[leftIdx].Count(n => n == -1);
                int fileSize = groupedBlocks[rightIdx].Count;

                if (freeSpaceSize < fileSize)
                {
                    continue;
                }

                int freeSizeIndex = groupedBlocks[leftIdx].IndexOf(-1);
                List<int> leftSubsequence = groupedBlocks[leftIdx].GetRange(freeSizeIndex, fileSize);
                List<int> rightSubsequence = groupedBlocks[rightIdx].GetRange(0, fileSize);

                groupedBlocks[leftIdx].RemoveRange(freeSizeIndex, fileSize);
                groupedBlocks[leftIdx].InsertRange(freeSizeIndex, rightSubsequence);

                groupedBlocks[rightIdx].RemoveRange(0, fileSize);
                groupedBlocks[rightIdx].InsertRange(0, leftSubsequence);
            }

            rightIdx--;
        }

        blocks.Clear();
        blocks.AddRange(groupedBlocks.SelectMany(group => group));
    }

    private static List<List<int>> GroupByContiguous(List<int> numbers)
    {
        List<List<int>> groupedNumbers = [];
        List<int> currentGroup = [numbers[0]];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] == numbers[i - 1])
            {
                currentGroup.Add(numbers[i]);
            }
            else
            {
                groupedNumbers.Add(new List<int>(currentGroup));
                currentGroup.Clear();
                currentGroup.Add(numbers[i]);
            }
        }

        if (currentGroup.Count > 0)
        {
            groupedNumbers.Add(currentGroup);
        }

        return groupedNumbers;
    }

    private static long GetChecksum(List<int> diskmap)
    {
        return diskmap
            .Select((value, index) => value != -1 ? (long)index * value : 0)
            .Sum();
    }
}
