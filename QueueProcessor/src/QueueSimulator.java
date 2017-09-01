import java.util.Queue;
import java.util.Scanner;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public final class QueueSimulator implements Runnable {
    private final Queue<String> queue;

    private volatile boolean isRunning = true;

    private QueueSimulator(Queue<String> queue) {
        assert queue != null;
        this.queue = queue;
    }

    @Override
    public void run() {
        while (isRunning) {
            queue.offer(RandomGenerator.generateRandomBigInteger());
        }
    }

    private void shutdown() {
        isRunning = false;
    }

    public static void main(String[] args) throws InterruptedException {
        final String cutoff = RandomGenerator.generateRandomBigInteger();

        ExecutorService executorService = Executors.newFixedThreadPool(6);

        Queue<String> queue = new ConcurrentLinkedQueue<>();

        // Create a few threads to populate the queue
        QueueSimulator[] queueSimulators = new QueueSimulator[4];
        for (int i = 0; i < queueSimulators.length; i++) {
            queueSimulators[i] = new QueueSimulator(queue);
            executorService.submit(queueSimulators[i]);
        }

        // And another few threads to process the queue
        QueueProcessor[] queueProcessors = new QueueProcessor[2];
        for (int i = 0; i < queueProcessors.length; i++) {
            queueProcessors[i] = new QueueProcessorImpl(queue, cutoff);
            executorService.submit(queueProcessors[i]);
        }

        Scanner scanner = new Scanner(System.in);

        while (true) {
            System.out.println();
            System.out.println("Options:");
            System.out.println("1: Get count");
            System.out.println("2: Get min");
            System.out.println("3: Get max");
            System.out.println("Anything else to exit");

            String choice = scanner.next();
            if (choice.equals("1")) {
                for (int i = 0; i < queueProcessors.length; i++) {
                    System.out.println("Processor #" + (i + 1) + " count: " + queueProcessors[i].getCountLargerThanCutOff());
                }
            } else if (choice.equals("2")) {
                for (int i = 0; i < queueProcessors.length; i++) {
                    System.out.println("Processor #" + (i + 1) + " min: " + queueProcessors[i].getMin());
                }
            } else if (choice.equals("3")) {
                for (int i = 0; i < queueProcessors.length; i++) {
                    System.out.println("Processor #" + (i + 1) + " max: " + queueProcessors[i].getMax());
                }
            } else {
                break;
            }
        }

        System.out.println("Shutting down...");
        for (QueueSimulator queueSimulator : queueSimulators) {
            queueSimulator.shutdown();
        }
        for (QueueProcessor queueProcessor : queueProcessors) {
            queueProcessor.shutdown();
        }

        executorService.shutdown();
    }

}
