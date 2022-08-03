import React from "react";

export const StyleCard = ({style, handleDelete}) => {


return (
<>


<h3>{style.name}</h3>
<button onClick={() => handleDelete(style.id)}>Delete</button>
</>
)

}

export default StyleCard