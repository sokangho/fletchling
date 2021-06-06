import { useContext } from 'react';

import Timeline from '@/components/Timeline/Timeline';
import TimelineContext from '@/components/TimelineContext';

const TimelineList = () => {
  const { savedTimelines } = useContext(TimelineContext);

  return (
    <div className='flex flex-row flex-wrap gap-5'>
      {savedTimelines.map((username: string) => (
        <Timeline username={username} key={username} />
      ))}
    </div>
  );
};

export default TimelineList;
