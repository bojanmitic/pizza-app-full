import React from 'react';
import Button from '../Button';

interface IProps {
  title: string;
  body: any;
}

const Modal: React.FC<IProps> = ({ title, body }): JSX.Element => {
  return (
    <div className="fixed left-0 right-0 top-0 bottom-0 bg-gray-400 flex items-center justify-center">
      <div>
        <div>
          <h4>{title}</h4>
        </div>
        <div>{body}</div>
        <div>
          <Button />
        </div>
      </div>
    </div>
  );
};

export default Modal;
