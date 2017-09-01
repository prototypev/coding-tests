import java.util.Random;

final class RandomGenerator {

    private static final char[] PATTERN = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

    /**
     * @return A random BigInteger with between 101 to 150 bits.
     */
    static String generateRandomBigInteger() {
        Random random = new Random();


        int length = 101 + random.nextInt(50);
        char[] value = new char[length];

        int index = 0;
        boolean isNegative = random.nextBoolean();
        if (isNegative) {
            value[0] = '-';
            index = 1;
        }

        while (index < length) {
            value[index++] = PATTERN[random.nextInt(PATTERN.length)];
        }

        return new String(value);
    }
}
