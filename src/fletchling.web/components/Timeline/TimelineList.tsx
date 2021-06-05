import { useContext } from 'react';

import Timeline from '@/components/Timeline/Timeline';
import TimelineContext from '@/components/TimelineContext';

const TimelineList = () => {
  const { savedTimelines } = useContext(TimelineContext);

  return (
    <div>
      {savedTimelines.map((username: string) => (
        <Timeline username={username} key={username} />
      ))}
    </div>
  );
};

export default TimelineList;
