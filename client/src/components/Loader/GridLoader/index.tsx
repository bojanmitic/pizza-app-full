import React from 'react';
import BeatLoader from 'react-spinners/BeatLoader';

type IProps = {
  size: number;
  loading: boolean;
  styles?: string;
  color: string;
};

export const Loader = ({
  size,
  color,
  loading,
  styles,
}: IProps): JSX.Element => (
  <div className={`flex ${styles}`}>
    <div className="m-auto">
      <BeatLoader size={size} color={color} loading={loading} />
    </div>
  </div>
);
