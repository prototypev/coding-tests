import java.util.Arrays;
import java.util.Queue;

public class QueueProcessorImpl implements QueueProcessor {

    private final Queue<String> queue;
    private final char[] cutoff;

    private volatile boolean stopProcessing;
    private volatile long countLargerThanCutOff;

    // Assumption:
    // Reads are less often than writes. So the zero-stripping is done during read-time.
    // If reads are supposed to be more often than writes, then the min and max should store the zero-stripped values.
    private volatile char[] min;
    private volatile char[] max;

    public QueueProcessorImpl(Queue<String> queue, String cutoff) {
        assert queue != null;
        this.queue = queue;
        this.cutoff = cutoff.toCharArray();
    }

    @Override
    public void run() {
        while (!stopProcessing) {
            process();
        }
    }

    @Override
    public void shutdown() {
        stopProcessing = true;
    }

    @Override
    public String getMax() {
        if (max == null) {
            return null;
        }

        return new String(getZeroStrippedValue(max));
    }

    @Override
    public String getMin() {
        if (min == null) {
            return null;
        }

        return new String(getZeroStrippedValue(min));
    }

    @Override
    public long getCountLargerThanCutOff() {
        return countLargerThanCutOff;
    }

    void process() {
        String input = queue.poll();
        if (input != null) {
            process(input.toCharArray());
        }
    }

    private synchronized void process(char[] value) {
        if (compare(value, cutoff) > 0) {
            countLargerThanCutOff++;
        }

        if (min == null || compare(value, min) < 0) {
            min = value;
        }

        if (max == null || compare(value, max) > 0) {
            max = value;
        }
    }

    /**
     * Compares 2 character arrays and determines if one is larger than the other.
     *
     * @param value1
     * @param value2
     * @return 0 if value1 == value2; -1 if value1 < value2; 1 if value1 > value2
     */
    private static int compare(char[] value1, char[] value2) {
        if (value1 == null || value1.length == 0 || value2 == null || value2.length == 0) {
            throw new IllegalArgumentException("Inputs cannot be null or empty");
        }

        boolean isValue1Negative = value1[0] == '-';
        boolean isValue2Negative = value2[0] == '-';

        if (isValue1Negative && !isValue2Negative) {
            return -1;
        } else if (!isValue1Negative && isValue2Negative) {
            return 1;
        }
        // Otherwise values are both positive, or both negative

        // Strip leading 0s
        int value1FirstNonZeroIndex = getFirstNonZeroIndex(value1);
        int value2FirstNonZeroIndex = getFirstNonZeroIndex(value2);

        int value1Length = value1.length - value1FirstNonZeroIndex;
        int value2Length = value2.length - value2FirstNonZeroIndex;

        if (value1Length > value2Length) {
            return isValue1Negative ? -1 : 1;
        } else if (value1Length < value2Length) {
            return isValue1Negative ? 1 : -1;
        } else {
            for (int i = value1FirstNonZeroIndex; i < value1Length; i++) {
                if (value1[i] > value2[i]) {
                    return isValue1Negative ? -1 : 1;
                } else if (value1[i] < value2[i]) {
                    return isValue1Negative ? 1 : -1;
                }
            }

            return 0;
        }
    }

    private static int getFirstNonZeroIndex(char[] input) {
        if (input == null || input.length == 0) {
            throw new IllegalArgumentException("Input cannot be null or empty");
        }

        // Strip leading zeroes
        int firstNonZeroIndex = input[0] == '-' ? 1 : 0;
        while (firstNonZeroIndex < input.length) {
            if (input[firstNonZeroIndex] != '0') {
                return firstNonZeroIndex;
            }
            firstNonZeroIndex++;
        }

        // This is an all 0 array - equivalent to the value of just 0. So just send back the last index.
        return firstNonZeroIndex - 1;
    }

    private static char[] getZeroStrippedValue(char[] input) {
        if (input == null || input.length == 0) {
            throw new IllegalArgumentException("Input cannot be null or empty");
        }

        boolean isNegative = input[0] == '-';

        int firstNonZeroIndex = getFirstNonZeroIndex(input);
        if (isNegative) {
            firstNonZeroIndex--;
        }

        if (firstNonZeroIndex <= 0) {
            return input;
        }

        char[] zeroStrippedValue = Arrays.copyOfRange(input, firstNonZeroIndex, input.length);
        if (isNegative) {
            zeroStrippedValue[0] = '-';
        }

        return zeroStrippedValue;
    }
}
