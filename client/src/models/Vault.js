export class Vault {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.creator = data.creator
        this.isPrivate = data.isPrivate
        this.img = data.img
        this.description = data.description
        this.creatorId = data.creatorId

    }
}