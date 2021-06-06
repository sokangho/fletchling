import { useContext } from 'react';

import TimelineContext from '@/components/Context/TimelineContext';
import Timeline from '@/components/Timeline/Timeline';

const TimelineList = () => {
  const { savedTimelines } = useContext(TimelineContext);

  return (
    <div className='flex flex-row flex-wrap gap-5 my-10'>
      {savedTimelines.map((username: string) => (
        <Timeline username={username} key={username} />
      ))}
    </div>
  );
};

export default TimelineList;
