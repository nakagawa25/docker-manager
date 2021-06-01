const headers = {
    'Content-Type': 'application/json'
}



// *** Functions Container ***
async function createContainer() {
    try {

        let nameContainer = document.getElementById('nameContainer').value;
        let imageName = document.getElementById('format').value;

        let url = "https://localhost:5001/v1/containers"

        body = {
            "containerName": nameContainer,
            "imageName": imageName
        }

        let request = new XMLHttpRequest()
        request.open("POST", url, true)
        request.setRequestHeader("Content-type", "application/json")
        request.send(JSON.stringify(body))
        request.onload = function () {
            console.log(this.responseText)
            if (this.status === 200) {
                alert("Container criado com sucesso")
            }
        }

        return request.responseText
    }
    catch (error) {
        alert(error)
    }
}

async function deleteContainer() {
    try {

        let containerId = document.getElementById('containerId').value;
        let url = "https://localhost:5001/v1/containers"

        body = 
            containerId
        

        console.log(containerId)
        console.log(body)

        let request = new XMLHttpRequest()
        request.open("DELETE", url, true)
        request.setRequestHeader("Content-type", "application/json")
        request.send(JSON.stringify(body))
        request.onload = function () {
            console.log(this.responseText)
            if (this.status === 200) {
                alert("Container deletado com sucesso")
            }
        }
    } catch (error) {
        alert(error)
    }
}

async function deleteContainerAll() {
    try {
        let url = "https://localhost:5001/v1/containers/all"

        let request = new XMLHttpRequest()
        request.open("DELETE", url, true)
        request.onload = function () {
            console.log(this.responseText)
            if (this.status === 200) {
                alert("Todos os containers foram deletados")
            }
            else{
                console.error(this.responseText)
            }
        }
        request.send(null)
    } catch (error) {
        console.log(error)
    }
}

// *** Functions Image ***
async function pullImage() {
    try {
        let imageName = document.getElementById('imageName').value;
        let imageTag = document.getElementById('imageTag').value;

        let url = "https://localhost:5001/v1/images"

        body = {
            "imageName": imageName,
            "tag": imageTag
        }

        let request = new XMLHttpRequest()
        request.open("POST", url, true)
        request.setRequestHeader("Content-type", "application/json")
        request.send(JSON.stringify(body))

        request.onload = function () {
            if (this.status === 200) {
                alert("Imagem criada com sucesso")
            }
        }

        return request.responseText
    }
    catch (error) {
        console.log(error)
    }
}

async function deleteAllImages() {
    try {
        let url = "https://localhost:5001/v1/images/all"

        let request = new XMLHttpRequest()
        request.open("DELETE", url, true)
        request.onload = function () {
            console.log(this.responseText)
            if (this.status === 200) {
                alert("Todos as imagens foram deletadas")
            }
            else{
                console.error(this.responseText)
            }
        }
        request.send(null)
    } catch (error) {
        console.log(error)
    }
}

async function deleteImage() {
    try {

        let imageId = document.getElementById('imageId').value;
        let url = "https://localhost:5001/v1/images"

        body =  imageId
        
        let request = new XMLHttpRequest()
        request.open("DELETE", url, true)
        request.setRequestHeader("Content-type", "application/json")
        request.send(JSON.stringify(body))
        request.onload = function () {
            console.log(this.responseText)
            if (this.status === 200) {
                alert("Imagem deletada com sucesso")
            }
        }
    } catch (error) {
        alert(error)
    }
}

function main() {
    data = getImage("https://localhost:5001/v1/images")
    imagens = JSON.parse(data)
    console.log(imagens)
}

// pullImage()
// main()   