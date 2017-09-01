import org.junit.Before;
import org.junit.Test;

import java.util.ArrayDeque;
import java.util.Queue;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;

public class QueueProcessorTest {

    private QueueProcessorImpl queueProcessor;

    @Before
    public void before() {
        Queue<String> queue = new ArrayDeque<>();
        queue.offer("123");
        queue.offer("-0456");
        queue.offer("0789");
        queue.offer("00000");

        queueProcessor = new QueueProcessorImpl(queue, "-321");

        while (!queue.isEmpty()) {
            queueProcessor.process();
        }
    }

    @Test
    public void testGetMax() {
        assertThat(queueProcessor.getMax(), is("789"));
    }

    @Test
    public void testGetMin() {
        assertThat(queueProcessor.getMin(), is("-456"));
    }

    @Test
    public void testGetCountLargerThanCutOff() {
        assertThat(queueProcessor.getCountLargerThanCutOff(), is(3L));
    }

}