import { useState } from "react";
import { ICreateCategory } from "./types";  

const CategoryCreatePage= () => {

const [state, setState] = useState<ICreateCategory>({
    name: ""
})

    //метод обробника подій назви
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>)=>{
        console.log("Input set name", e.target.name);
        console.log("Input set value", e.target.value);
        setState({
            ...state,
             [e.target.name]: e.target.value
    })
    }

    //метод обробника подій файлу фото
    const handleFilesChange = (e: React.ChangeEvent<HTMLInputElement>)=>{
        console.log("Input files", e.target.files);
    if (e.target.files) 
    {
    const file = e.target.files[0];
    setState({
        ...state,
        [e.target.name]: file
    });    
    }
    }

    return (
        <>
        <h1>Додати категорію</h1>

        <form>
                <div className="mb-3">
                    <label htmlFor="name" className="form-label">Вкажіть назву</label>
                    <input type="email" className="form-control" id="name" name="name" 
                    value={state.name}
                    onChange={handleInputChange}/>
                </div>
                <div className="mb-3">
                    <label htmlFor="file" className="form-label">Оберіть фото</label>
                    <input className="form-control" type="file" name="file" id="file" 
                    accept="image/*"
                    onChange={handleFilesChange}/>
                </div>
                <button type="submit" className="btn btn-primary">Дадати</button>
            </form>

        </>
    );
}
export default CategoryCreatePage;