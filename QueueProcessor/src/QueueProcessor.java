public interface QueueProcessor extends Runnable {

    /**
     * Shuts down the queue processor;
     */
    void shutdown();

    /**
     * @return The maximum value seen thus far.
     */
    String getMax();

    /**
     * @return The minimum value seen thus far.
     */
    String getMin();

    /**
     * @return The number of values greater than the cut off value.
     */
    long getCountLargerThanCutOff();
}
