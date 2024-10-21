extends Sprite2D

var cpt = 0

#@export var taille = 0.01

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	print ("Hello world")
	position = Vector2(100,200)
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	affiche_cpt()
	
	# scale += Vector2(taille,taille)
	


func affiche_cpt()-> void:
	cpt = cpt + 1
	print(cpt)
