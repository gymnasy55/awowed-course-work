import React from 'react'
import {PopularItem} from "./PopularItem/PopularItem";

export const Popular = () => {
  const items = ['Кольцо', 'Цепочка', 'Серьги', 'Цепочка']

  return (
    <>
      <h2>Популярні коштовності</h2>
      <div className="row">
        { items.map((item, idx) => <PopularItem title={item} idx={idx} /> ) }
      </div>
    </>
  )
}