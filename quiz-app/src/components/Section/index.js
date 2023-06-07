import React from 'react'

import cn from 'classnames'
import PropTypes from 'prop-types'

import GLOBALS from '../../app-globals'

import Text from '../Text'
import textTypes from '../Text/constants/textTypes'

import styles from './styles.module.scss'

const Section = ({ id, children, className, titleClassName, title }) => (
  <section className={cn(styles.Section, className)} id={id}>
    {title && (
      <Text
        className={cn(styles.Section_title, titleClassName)}
        colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL['400']}
        id={id ? `${id}-title` : null}
        type={textTypes.HEADING.XXXS}
      >
        {title}
      </Text>
    )}
    {children}
  </section>
)

Section.defaultProps = {
  id: null,
  className: null,
  children: null,
  title: null
}

Section.propTypes = {
  id: PropTypes.string,
  className: PropTypes.string,
  titleClassName: PropTypes.string,
  children: PropTypes.any,
  title: PropTypes.string
}

export default Section
