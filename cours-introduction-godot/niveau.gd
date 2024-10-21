extends Node2D


@export var taille = 0.01
@export var vitesse = 2
var modif = 5

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	scaling_moving_logo()
	controle_robot()
	
	

func scaling_moving_logo()->void:
	$joueur/gamagora.scale += Vector2(taille,taille)
	
	if ($joueur/gamagora.scale.x >= 0.7):
		taille = -0.01
	elif ($joueur/gamagora.scale.x <= 0.4):
		taille = 0.01
		
	$joueur/gamagora.position.x += modif * vitesse
	if ($joueur/gamagora.position.x >= 600):
		modif = -5
	elif ($joueur/gamagora.position.x <= 100):
		modif = 5


func controle_robot()->void:
	var input_direction = Input.get_vector("Gauche", "Droite", "Haut", "Bas")
	$tete.position += input_direction*(vitesse*4)
	
