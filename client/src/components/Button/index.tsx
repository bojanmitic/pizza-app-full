import React from 'react';
import { Loader } from '../Loader/GridLoader/index';

interface IButtonProps {
  //Common HTML Props
  id?: string;
  className?: string;
  onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void | Promise<any>;
  title?: string;
  disabled?: boolean;
  type?: 'button' | 'submit' | 'reset';

  //Button component props
  pending?: boolean;
  icon?: string;
  loaderSize?: number;
  loaderColor?: string;
  loaderStyles?: string;
  labelStyles?: string;
}

const Button = (props: IButtonProps) => {
  const {
    disabled,
    pending,
    id,
    className,
    title,
    type = 'button',
    icon,
    onClick,
    loaderSize = 10,
    loaderColor = 'blue',
    loaderStyles,
    labelStyles,
  } = props;

  const rootProps = {
    id,
    title,
    type,
    className,
    disabled: disabled || pending,
  };
  return (
    <button
      {...rootProps}
      className={`${className} inline-block text-center cursor-pointer bg-none mt-1 focus:outline-none`}
      onClick={onClick}
    >
      <span className="flex relative justify-center">
        {pending && (
          <span className={`${loaderStyles} absolute self-center`}>
            <Loader color={loaderColor} size={loaderSize} loading />
          </span>
        )}
        {icon && (
          <span className="self-center mr-2">
            <i className={icon}></i>
          </span>
        )}
        <span className={labelStyles}>{title}</span>
      </span>
    </button>
  );
};

export default Button;
